﻿//-----------------------------------------------------------------------
// <copyright file= "HomeController.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Date Created: 2018/2/10 星期六 15:47:25
// Modified by:
// Description: Instructor-Home-控制器
//-----------------------------------------------------------------------
using Controllers.PSU.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSU.EFCore;
using PSU.IService.Areas.Instructor;
using PSU.Model.Areas.Instructor.Home;
using PSU.Utility;
using PSU.Utility.Web;
using System;
using System.Threading.Tasks;

namespace Controllers.PSU.Areas.Instructor
{
    [Area("Instructor")]
    [Authorize(Policy = "Instructor")]
    public class HomeController : DanvicController
    {
        #region Initialize

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IHomeService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IHomeService service, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _service = service;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            CurrentUser.Configure(_httpContextAccessor);
        }

        #endregion

        #region View

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var webModel = await _service.InitIndexPageAsync(_context);
            return View(webModel);
        }

        /// <summary>
        /// 公告列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Bulletin()
        {
            return View();
        }

        /// <summary>
        /// 公告详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("Bulletin");
            }

            var model = await _service.GetBulletinDetailAsync(Convert.ToInt64(id), _context);

            return View(model);
        }

        #endregion

        #region Service

        /// <summary>
        /// 获取折线图数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLineChart()
        {
            var chart = await _service.InitLineChartAsync(_context);
            return Json(chart);
        }

        /// <summary>
        /// 获取饼图数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPieChart()
        {
            var chart = await _service.InitPieChartAsync(_context);
            return Json(chart);
        }

        /// <summary>
        /// 公告页面搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            BulletinViewModel webModel = JsonUtility.ToObject<BulletinViewModel>(search);

            webModel = await _service.SearchBulletinAsync(webModel, _context);

            //Search Or Init
            bool flag = string.IsNullOrEmpty(webModel.STitle) && string.IsNullOrEmpty(webModel.SDateTime) && webModel.SType == 0;

            var returnData = new
            {
                data = webModel.BulletinList,
                limit = webModel.Limit,
                page = flag == true ? webModel.Page : 1,
                total = webModel.Total
            };

            return Json(returnData);
        }

        #endregion
    }
}
