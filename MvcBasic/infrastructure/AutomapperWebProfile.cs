using MvcBasic.Models;
using MvcTuto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBasic.infrastructure 
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {

            CreateMap<EmployeeDomainModelWeb, EmployeeViewModel>()

                .ForMember(dest => dest.ExtraValue, opt => opt.MapFrom(src => src.ExtraValue.Encrypt()))
                .ForMember(dest => dest.CurrentDate, opt => opt.MapFrom(src => src.CurrentDate.ToString("MM/dd/yyy hh:mm")));
            CreateMap<EmployeeDomainModel, EmployeeViewModel>();

            //Reverese mapping

            CreateMap<EmployeeViewModel, EmployeeDomainModelWeb>();
            CreateMap<EmployeeDomainModel, EmployeeViewModel>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();


            });
        }


    }

    public static class ExtensionMethod
    {

        public static string Encrypt(this Int32 num)
        {

            return "Technotips:" + num;
        }
    }
}