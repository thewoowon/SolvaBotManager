using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SolvaBot.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.FileProviders;

namespace SolvaBot.Data
{
    public class ParameterFinder
    {
        private string _companyCode = string.Empty;
        private string _companyName = string.Empty;
        SOLVABOTContext context;
        

        public SOLVABOTContext Context
        {
            get { return context; }
        }

        public ParameterFinder()
        {
            context = new SOLVABOTContext();
        }      
        
        public string GetCompanyCode()
        {
            return _companyCode;
        }

        public string GetCompanyName()
        {
            return _companyName;
        }

        public string SetCompanyCode(NavigationManager nm)
        {
            if(string.IsNullOrEmpty(_companyCode))
            {
                var uri = nm.ToAbsoluteUri(nm.Uri);

                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("companyCode", out var _queryCompanyCode))
                {
                    _companyCode = _queryCompanyCode;
                }
                else
                {
                    _companyCode = string.Empty; // 오류 페이지 라우팅
                }

                _companyName = (from e in Context.TblComCompany
                                where e.CompanyCode == _companyCode
                                select e.CompanyName).FirstOrDefault();
            }

            return _companyCode;
        }

        public Task<TblComCategory[]> GetComCategories()
        {
            TblComCategory[] category;

            category = (from e in Context.TblComCategory
                        where e.CompanyCode == _companyCode
                        select e).ToArray();

            return Task.FromResult(category) ;
        }

        public async void AddComCategories()
        {
            TblComCategory newCategory = new TblComCategory();

            newCategory.Code = "ABCD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            newCategory.CompanyCode = _companyCode;
            newCategory.Title = "";
            newCategory.Wdate = DateTime.Now;

            await this.Context.TblComCategory.AddAsync(newCategory);
            this.Context.SaveChanges();
        }

        public void DeleteComCategories(TblComCategory category)
        {
            this.Context.TblComCategory.Remove(category);
            this.Context.SaveChanges();
        }

        public void UpdateComCategories(TblComCategory beforeCategory, TblComCategory afterCategory)
        {
            this.Context.TblComCategory.Remove(beforeCategory);
            this.Context.TblComCategory.Update(afterCategory);
            this.Context.SaveChanges();
        }

        public Task<TblComMenu[]> GetComMenu(string categoryCode)
        {
            TblComMenu[] menu;

            menu = (from e in Context.TblComMenu
                        where e.CategoryCode == categoryCode
                        select e).ToArray();

            return Task.FromResult(menu);
        }

        public Task<TblComMenu[]> GetComMenu()
        {
            TblComMenu[] menu;

            menu = (from e in Context.TblComMenu
                    where e.CompanyCode == _companyCode
                    select e).ToArray();

            return Task.FromResult(menu);
        }

        public async void AddComMenu()
        {
            TblComMenu newMenu = new TblComMenu();

            newMenu.Code = "ABCD" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            newMenu.CompanyCode = _companyCode;
            newMenu.Title = "";
            newMenu.Wdate = DateTime.Now;
            newMenu.CategoryCode = "";

            await this.Context.TblComMenu.AddAsync(newMenu);
            this.Context.SaveChanges();
        }

        public async void AddComMenu(string categoryCode)
        {
            TblComMenu newMenu = new TblComMenu();

            newMenu.Code = "EFGH" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            newMenu.CompanyCode = _companyCode;
            newMenu.Title = "";
            newMenu.Wdate = DateTime.Now;
            newMenu.CategoryCode = categoryCode;

            await this.Context.TblComMenu.AddAsync(newMenu);
            this.Context.SaveChanges();
        }

        public void DeleteComMenu(TblComMenu menu)
        {
            this.Context.TblComMenu.Remove(menu);
            this.Context.SaveChanges();
        }

        public void UpdateComMenu(TblComMenu beforeMenu, TblComMenu afterMenu)
        {
            this.Context.TblComMenu.Remove(beforeMenu);
            this.Context.TblComMenu.Update(afterMenu);
            this.Context.SaveChanges();
        }

        public Task<TblComMenuDetail[]> GetComMenuDetail (string menuCode)
        {
            TblComMenuDetail[] menuDetail;

            menuDetail = (from e in Context.TblComMenuDetail
                        where e.MenuCode == menuCode
                          select e).ToArray();

            return Task.FromResult(menuDetail);
        }

        public Task<TblComMenuDetail[]> GetComMenuDetail()
        {
            TblComMenuDetail[] menuDetail;

            menuDetail = (from e in Context.TblComMenuDetail
                          where e.CompanyCode == _companyCode
                          select e).ToArray();


            return Task.FromResult(menuDetail);
        }

        public Task<TblComAddfiles[]> GetComMenuDetailImageFile(string detailCode)
        {
            TblComAddfiles[] menuDetailImageFile;

            menuDetailImageFile = (from e in Context.TblComAddfiles
                                        where e.Pcode == detailCode && e.Role == 1
                                   select e).ToArray();

            return Task.FromResult(menuDetailImageFile);
        }
        public Task<TblComAddfiles[]> GetComMenuDetailDocuFile(string detailCode)
        {
            TblComAddfiles[] menuDetailDocuFile;

            menuDetailDocuFile = (from e in Context.TblComAddfiles
                          where e.Pcode == detailCode && e.Role == 2
                                  select e).ToArray();

            return Task.FromResult(menuDetailDocuFile);
        }

        public async void AddComDetail()
        {
            TblComMenuDetail newDetail = new TblComMenuDetail();

            newDetail.Code = "IJKL" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            newDetail.CompanyCode = _companyCode;
            newDetail.Title = "";
            newDetail.SubTitle = "";
            newDetail.Content = "";
            newDetail.Wdate = DateTime.Now;
            newDetail.MenuCode = "";

            await this.Context.TblComMenuDetail.AddAsync(newDetail);
            this.Context.SaveChanges();
        }

        public async void AddComDetail(string menuCode)
        {
            TblComMenuDetail newDetail = new TblComMenuDetail();

            newDetail.Code = "IJKL" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            newDetail.CompanyCode = _companyCode;
            newDetail.Title = "";
            newDetail.SubTitle = "";
            newDetail.Content = "";
            newDetail.Wdate = DateTime.Now;
            newDetail.MenuCode = menuCode;

            await this.Context.TblComMenuDetail.AddAsync(newDetail);
            this.Context.SaveChanges();
        }

        public void DeleteComDetail(TblComMenuDetail detail)
        {
            this.Context.TblComMenuDetail.Remove(detail);
            this.Context.SaveChanges();
        }

        public void UpdateComDetail(TblComMenuDetail beforeDetail, TblComMenuDetail afterDetail)
        {
            this.Context.TblComMenuDetail.Remove(beforeDetail);
            this.Context.TblComMenuDetail.Update(afterDetail);
            this.Context.SaveChanges();
        }


        public async Task UpdateComDetailFiles(TblComAddfiles imageFile, TblComAddfiles docuFile, string detailCode)
        {
            TblComAddfiles[] menuFiles;

            menuFiles = (from e in Context.TblComAddfiles
                         where e.Pcode == detailCode
                         select e).ToArray();

            foreach (var item in menuFiles)
            {
                Context.TblComAddfiles.Remove(item);
            }

            if(!string.IsNullOrEmpty(imageFile.Code))
            {
                await Context.TblComAddfiles.AddAsync(imageFile);
            }

            if(!string.IsNullOrEmpty(docuFile.Code))
            {
                await Context.TblComAddfiles.AddAsync(docuFile);
            }

            this.Context.SaveChanges();
        }

        public List<IFileInfo> ImageFileSetting(string code,IWebHostEnvironment env)
        {

            string detailCode = code; // Detail Code
            TblComAddfiles[] menuFiles;
            List<IFileInfo> list = new List<IFileInfo>();

            menuFiles = (from e in Context.TblComAddfiles
                         where e.Pcode == detailCode && e.Role == 1
                         select e).ToArray();

            foreach (var item in menuFiles)
            {
                if(File.Exists(item.Floc))
                {
                    var provider = new PhysicalFileProvider(env.WebRootPath);
                    var file = provider.GetFileInfo(Path.Combine(item.Fn));
                    list.Add(file);
                }       
            }

            return list;
        }
        public List<IFileInfo> DocuFilesSetting(string code, IWebHostEnvironment env)
        {
            string detailCode = code; // Detail Code
            TblComAddfiles[] menuFiles;
            List<IFileInfo> list = new List<IFileInfo>();

            menuFiles = (from e in Context.TblComAddfiles
                         where e.Pcode == detailCode && e.Role == 2
                         select e).ToArray();

            foreach (var item in menuFiles)
            {
                if (File.Exists(item.Floc))
                {
                    var provider = new PhysicalFileProvider(env.WebRootPath);
                    var file = provider.GetFileInfo(Path.Combine(item.Fn));
                    list.Add(file);
                }
            }

            return list;
        }
    }
}
