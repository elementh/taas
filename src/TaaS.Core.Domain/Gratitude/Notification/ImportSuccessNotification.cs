﻿using System;
using MediatR;

namespace TaaS.Core.Domain.Gratitude.Notification
{
    public class ImportSuccessNotification : INotification
    {
        public ImportSuccessNotification(DateTime started)
        {
            Started = started;
            Finished = DateTime.UtcNow;
        }

        public DateTime Started { get; }
        public DateTime Finished { get; }
    }
}