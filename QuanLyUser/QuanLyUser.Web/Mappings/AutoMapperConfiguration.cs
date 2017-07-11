using AutoMapper;
using QuanLyUser.Model.Models;
using QuanLyUser.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyUser.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>().MaxDepth(2);
            });
        }
    }
}