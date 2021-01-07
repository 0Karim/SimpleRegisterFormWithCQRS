using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Mapping
{
    public interface IMapFrom<T>
    {
        public void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
