﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EdgeSharp.Blazor
{
    internal class EdgeSharpDispatcher : Dispatcher
    {
        private readonly EdgeSharpSyncrhronizationContext _context;

        public EdgeSharpDispatcher()
        {
            _context = new EdgeSharpSyncrhronizationContext();
            _context.UnhandledException += (sender, e) =>
            {
                OnUnhandledException(e);
            };
        }

        public override bool CheckAccess() => SynchronizationContext.Current == _context;

        public override Task InvokeAsync(Action workItem)
        {
            if (CheckAccess())
            {
                workItem();
                return Task.CompletedTask;
            }

            return _context.InvokeAsync(workItem);
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            if (CheckAccess())
            {
                return workItem();
            }

            return _context.InvokeAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            if (CheckAccess())
            {
                return Task.FromResult(workItem());
            }

            return _context.InvokeAsync<TResult>(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            if (CheckAccess())
            {
                return workItem();
            }

            return _context.InvokeAsync<TResult>(workItem);
        }
    }
}
