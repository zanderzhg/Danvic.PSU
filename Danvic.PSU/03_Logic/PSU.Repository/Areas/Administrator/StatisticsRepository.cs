﻿//-----------------------------------------------------------------------
// <copyright file= "StatisticsRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Date Created: 2018-04-02 21:24:00
// Modified by:
// Description: Administrator-Statistics-功能实现仓储
//-----------------------------------------------------------------------
using LinqKit;
using Microsoft.EntityFrameworkCore;
using PSU.EFCore;
using PSU.Entity.SignUp;
using PSU.Model.Areas.Administrator.Statistics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSU.Repository.Areas.Administrator
{
    public class StatisticsRepository
    {
        #region Register API

        /// <summary>
        /// 根据搜索条件获取新生报名信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<Register>> GetListAsync(RegisterViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SMajorClass) && string.IsNullOrEmpty(webModel.SDate))
            {
                return await context.Set<Register>().Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
            }
            else
            {
                IQueryable<Register> registers = context.Register.AsQueryable();

                var predicate = PredicateBuilder.New<Register>();

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //专业班级名称
                if (!string.IsNullOrEmpty(webModel.SMajorClass))
                {
                    predicate = predicate.And(i => i.MajorClass.Contains(webModel.SMajorClass));
                }

                //预计到校时间
                if (!string.IsNullOrEmpty(webModel.SDate))
                {
                    predicate = predicate.And(i => i.ArriveTime.ToString("yyyy-MM-dd") == webModel.SDate);
                }

                return await registers.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        #endregion

        #region Goods API

        /// <summary>
        /// 根据搜索条件获取物品预定信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<GoodsInfo>> GetListAsync(GoodsViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SGoodsName) && string.IsNullOrEmpty(webModel.SDate))
            {
                return await context.Set<GoodsInfo>().Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.ChosenTime).ToListAsync();
            }
            else
            {
                IQueryable<GoodsInfo> goodsInfos = context.GoodsInfo.AsQueryable();

                var predicate = PredicateBuilder.New<GoodsInfo>();

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.StudentName == webModel.SName);
                }

                //物品名称
                if (!string.IsNullOrEmpty(webModel.SGoodsName))
                {
                    predicate = predicate.And(i => i.GoodsName.Contains(webModel.SGoodsName));
                }

                //物品选择时间
                if (!string.IsNullOrEmpty(webModel.SDate))
                {
                    predicate = predicate.And(i => i.ChosenTime.ToString("yyyy-MM-dd") == webModel.SDate);
                }

                return await goodsInfos.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        #endregion

        #region Dormitory API

        /// <summary>
        /// 根据搜索条件获取物品预定信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<BunkInfo>> GetListAsync(DormitoryViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SBuilding) && string.IsNullOrEmpty(webModel.SStudent))
            {
                return await context.Set<BunkInfo>().Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
            }
            else
            {
                IQueryable<BunkInfo> bunkInfos = context.BunkInfo.AsQueryable();

                var predicate = PredicateBuilder.New<BunkInfo>();

                //宿舍名称
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.DormName == webModel.SName);
                }

                //宿舍楼名称
                if (!string.IsNullOrEmpty(webModel.SBuilding))
                {
                    predicate = predicate.And(i => i.BuildingName.Contains(webModel.SBuilding));
                }

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SStudent))
                {
                    predicate = predicate.And(i => i.StudentName.Contains(webModel.SStudent));
                }

                return await bunkInfos.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        #endregion

        #region Book API
        #endregion
    }
}
