﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using DBproject.Model;
using DBproject.context;

namespace DBproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private coreDbContext dbContext;
        public usersController(coreDbContext _context)
        {
            dbContext = _context;
        }

        // GET users
        // api/users
        [HttpGet]
        public dataRetuenMessage Users(dynamic _in)
        {
            dataRetuenMessage result = new dataRetuenMessage();

            var sAll = from m in dbContext.user_data
                       select m;
            int pageNum = _in.page_num;
            int pageSize = _in.page_size;

            sAll = sAll.Skip((pageNum - 1) * pageSize).Take(pageSize);
            result.code = 1;
            result.message = "查询成功";
            result.data = new user_data[pageSize];
            int i = 0;
            foreach(var p in sAll)
            {
                result.data[i++] = p;
            }
            return result;
        }
    }
}