﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Excellency.Reports.Controllers
{
    public class ReportController : Controller
    {
        private string UserConnectionString =  ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }
        #region KRA Report
        public ActionResult PrintPerformanceAppraisal(int id)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "dsAppraisal";
            ds.Tables.Add(KRAHeader(id));
            ds.Tables.Add(KRADetails(id));
            var ReportParth = Server.MapPath("~/Reports/PerformanceAppraisal.rpt");
            return new CrystalReportToPdf(ReportParth, ds);
        }
        private DataTable KRAHeader(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spKRAReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Header";
            return dt;
        }
        private DataTable KRADetails(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spKRAReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "KRA";
            return dt;
        }
        #endregion
        #region Behavioral Report
        public ActionResult PrintBehavioral(int id)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "dsAppraisal";
            ds.Tables.Add(BehavioralHeader(id));
            ds.Tables.Add(BehavioralDetails(id));
            var ReportParth = Server.MapPath("~/Reports/Behavioral.rpt");
            return new CrystalReportToPdf(ReportParth, ds);
        }
        private DataTable BehavioralHeader(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spBehavioralReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Header";
            return dt;
        }
        private DataTable BehavioralDetails(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spBehavioralReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Behavioral";
            return dt;
        } 
        #endregion
        public ActionResult PrintEmployeeInformation(int id)
        {
            DataSet ds = new DataSet();
            ds.DataSetName = "dsInformation";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEmployeeInformationReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "EmployeeInfo";
            ds.Tables.Add(dt);
            var ReportParth = Server.MapPath("~/Reports/EmployeeInformation.rpt");
            return new CrystalReportToPdf(ReportParth, ds);
        }
        public ActionResult PrintEmployeePerformance(int id,int period)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(PerformanceDetails(id, period));
            ds.Tables.Add(PerformanceHeader(id, period));
            var ReportParth = Server.MapPath("~/Reports/EmployeePerformance.rpt");
            return new CrystalReportToPdf(ReportParth, ds);
        }
        private DataTable PerformanceHeader(int id,int period)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEmployeePerformanceReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@SeasonId", period);
            cmd.Parameters.AddWithValue("@QueryType", 0);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Interpretation";
            return dt;
        }
        private DataTable PerformanceDetails(int id, int period)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spEmployeePerformanceReport]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@SeasonId", period);
            cmd.Parameters.AddWithValue("@QueryType", 1);
            DataTable dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Details";
            return dt;
        }

        public ActionResult PrintGraphicalDistribution(int period)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[spGraphicalDestribution]";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SeasonId", period);

            var dt = SCObjects.ExecGetData(cmd, UserConnectionString);
            dt.TableName = "Interpretation";
            ds.Tables.Add(dt);
            var ReportParth = Server.MapPath("~/Reports/GraphicalDistribution.rpt");
            return new CrystalReportToPdf(ReportParth, ds);
        }
    }
}