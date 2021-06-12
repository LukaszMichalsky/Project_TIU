using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

using zoo_manager_backend.Controllers;
using zoo_manager_backend.Models;


namespace zoo_manager_tests
{
   public class AnimalSpecimensController : ControllerBase
   {
        private readonly List<AnimalSpecimen> _AnimalSpecimen;
        public AnimalSpecimensController()
        {
            
        }
    }
}
