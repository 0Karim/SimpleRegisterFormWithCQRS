using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Common.Command
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
