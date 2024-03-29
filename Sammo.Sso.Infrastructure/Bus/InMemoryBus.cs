﻿using MediatR;
using Sammo.Sso.Domain.Core.Bus;
using Sammo.Sso.Domain.Core.Commands;
using Sammo.Sso.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sammo.Sso.Infrastructure.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        // 事件仓储服务
        //private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 实现我们在IMediatorHandler中定义的接口
        /// 没有返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task SendCommand<T>(T command) where T : Command
        {
            //这个是正确的
            return _mediator.Send(command);//请注意 入参 的类型

            //注意！这个仅仅是用来测试和研究源码的，请开发的时候不要使用这个
            //return Send(command);//请注意 入参 的类型
        }

        /// <summary>
        /// 引发事件的实现方法
        /// </summary>
        /// <typeparam name="T">泛型 继承 Event：INotification</typeparam>
        /// <param name="event">事件模型，比如StudentRegisteredEvent</param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            // 除了领域通知以外的事件都保存下来
            //if (!@event.MessageType.Equals("DomainNotification"))
            //    _eventStore?.Save(@event);

            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }
    }
}
