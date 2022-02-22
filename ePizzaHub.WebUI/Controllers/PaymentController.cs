﻿using ePizzaHub.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Helpers;
using ePizzaHub.UI.Helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ePizzaHub.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        IOptions<RazorPayConfig> _razorPayConfig;
        IPaymentService _paymentService;
        IUserAccessor _userAccessor;
        IOrderService _orderService;

        public PaymentController(IOptions<RazorPayConfig> razorPayConfig, IPaymentService paymentService,
            IUserAccessor userAccessor, IOrderService orderService)
        {
            _razorPayConfig = razorPayConfig;
            _paymentService = paymentService;
            _userAccessor = userAccessor;
            _orderService = orderService;
        }

        public User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }
        public IActionResult Index()
        {
            CartModel cart = TempData.Peek<CartModel>("Cart");
            PaymentModel payment = new PaymentModel();
            if (cart != null)
            {
                payment.Cart = cart;
                payment.GrandTotal = Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                string items = "";
                foreach (var item in cart.Items)
                {
                    items += item.Name + ",";
                }
                payment.Description = items;
                payment.Receipt = Guid.NewGuid().ToString();

                payment.RazorpayKey = _razorPayConfig.Value.Key;
                payment.OrderId = _paymentService.CreateOrder(payment.GrandTotal * 100, payment.Currency, payment.Receipt);
            }
            return View(payment);
        }

        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count > 0 && !string.IsNullOrWhiteSpace(form["rzp_paymentid"]))
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderId = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    var payment = _paymentService.GetPaymentDetails(paymentId);
                    bool IsSignVerified = _paymentService.VerifySignature(signature, orderId, paymentId);

                    if (IsSignVerified && payment != null)
                    {
                        CartModel cart = TempData.Get<CartModel>("Cart");
                        PaymentDetails model = new PaymentDetails();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;

                        model.Status = payment.Attributes["status"]; //captured
                        model.TransactionId = transactionId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.Id = paymentId;
                        model.UserId = CurrentUser.Id;

                        int status = _paymentService.SavePaymentDetails(model);
                        if (status > 0)
                        {
                            Response.Cookies.Append("CId", ""); //resettingg cartId in cookie

                            Address address = TempData.Get<Address>("Address");
                            _orderService.PlaceOrder(CurrentUser.Id, orderId, paymentId, cart, address);

                            //TO DO: Send email
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                        else
                        {
                            ViewBag.Message = "Although, due to some technical issues it's not get updated in our side. We will contact you soon..";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.Message = "Your payment has been failed. You can contact us at support@dotnettricks.com.";
            return View();
        }

        public IActionResult Receipt()
        {
            PaymentDetails model = TempData.Peek<PaymentDetails>("PaymentDetails");
            return View(model);
        }
    }
}
