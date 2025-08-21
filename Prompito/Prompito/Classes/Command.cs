﻿/*
 * 
 * Prompito
 * Version: v1.0.0
 * Description: Utilitário de git hooks
 * Author: rafaelsouzars
 * Github: https://github.com/rafaelsouzars
 * 
 */
using Prompito.AbstractClasses;

namespace Prompito.Classes
{
    class Command<Receiver> : AbstractCommandBase where Receiver : class
    {
        public delegate void ReceiverAction(Receiver receiver);

        private readonly Receiver _receiver;
        private readonly ReceiverAction _action;

        public Command(Receiver receiver, ReceiverAction action)
        {
            _receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Execute()
        {
            _action.Invoke(_receiver);
        }
    }
}
