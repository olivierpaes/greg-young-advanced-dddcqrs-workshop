﻿using System.Threading;

namespace Restaurant
{
    public class Cashier : IHandle<TakePayment>
    {
        private readonly IPublisher _publisher;
        private readonly IHorn _horn;

        public Cashier(IPublisher publisher, IHorn horn)
        {
            _publisher = publisher;
            _horn = horn;
        }

        public void Handle(TakePayment message)
        {
            var order = message.Order;
            _horn.Say($"[cashier]: taking payment for {order.OrderId}.");

            Thread.Sleep(500);

            order.IsPaid = true;

            _horn.Say($"[cashier]: took payment for {order.OrderId}.");

            _publisher.Publish(new OrderPaid(message, new Order(order)));
        }
    }
}
