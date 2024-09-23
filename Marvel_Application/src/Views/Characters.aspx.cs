using System;
using System.Linq;
using System.Web.UI;
using System.Threading.Tasks;
using Marvel_Application.Models;
using System.Collections.Generic;
using Marvel_Application.Controllers;
using Marvel_Application.Infrastructure;

namespace Marvel_Application.Views
{
    public partial class Characters : Page
    {
        private const int PageSize = 10;
        private const int TotalCharactersToFetch = 200;
        private const int CharactersPerCall = 100;
        private const int SelectCount = 50;

        private readonly ICharactersController _controller;

        public Characters()
        {
            _controller = DependencyResolver.Resolve<ICharactersController>();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCharactersAsync();
            }
        }

        private async Task LoadCharactersAsync()
        {
            var selectedCharacters = await _controller.GetSelectedCharactersAsync(TotalCharactersToFetch, SelectCount);

            if (selectedCharacters == null || selectedCharacters.Count == 0)
            {
                rptCharacters.DataSource = null;
                rptCharacters.DataBind();

                lblNoData.Visible = true; 
                rptCharacters.Visible = false; 
                lblPageInfo.Text = "No characters available.";
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                return;
            }

            Session["Characters"] = selectedCharacters;

            BindCharacters(selectedCharacters, 1);
        }

        public void BindCharacters(List<Character> characters, int pageIndex)
        {
            if (characters == null || characters.Count == 0)
            {
                rptCharacters.DataSource = null;
                rptCharacters.DataBind();
                lblPageInfo.Text = "0 de 0";
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                return;
            }

            int skip = (pageIndex - 1) * PageSize;
            var pagedCharacters = characters.Skip(skip).Take(PageSize).ToList();

            rptCharacters.DataSource = pagedCharacters;
            rptCharacters.DataBind();

            ViewState["PageIndex"] = pageIndex;
            UpdatePaginationControls(characters.Count, pageIndex, PageSize);
        }

        public void UpdatePaginationControls(int totalItems, int pageIndex, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            btnPrev.Enabled = pageIndex > 1;
            btnNext.Enabled = pageIndex < totalPages;

            lblPageInfo.Text = $"Page {pageIndex} of {totalPages}";
        }


        protected void BtnPrev_Click(object sender, EventArgs e)
        {
            if (ViewState["PageIndex"] != null && Session["Characters"] != null)
            {
                int pageIndex = (int)ViewState["PageIndex"];
                if (pageIndex > 1)
                {
                    pageIndex--;
                    var characters = (List<Character>)Session["Characters"];
                    BindCharacters(characters, pageIndex);
                }
            }
        }
        protected void BtnNext_Click(object sender, EventArgs e)
        {
            if (ViewState["PageIndex"] != null && Session["Characters"] != null)
            {
                int pageIndex = (int)ViewState["PageIndex"];
                var characters = (List<Character>)Session["Characters"];
                int totalPages = (int)Math.Ceiling(characters.Count / (double)PageSize);

                if (pageIndex < totalPages)
                {
                    pageIndex++;
                    BindCharacters(characters, pageIndex);
                }
            }
        }

        protected async void PerformSearch(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                await LoadCharactersAsync();
            }
            else
            {
                var filteredCharacters = await _controller.SearchCharactersAsync(searchTerm, SelectCount);

                if (filteredCharacters == null || !filteredCharacters.Any())
                {
                    rptCharacters.DataSource = null;
                    rptCharacters.DataBind();

                    lblNoData.Visible = true; 
                    rptCharacters.Visible = false;
                    lblPageInfo.Text = "No characters found.";
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                }
                else
                {
                    rptCharacters.DataSource = filteredCharacters;
                    rptCharacters.DataBind();

                    lblNoData.Visible = false; 
                    rptCharacters.Visible = true; 
                    lblPageInfo.Text = $"Page 1 of {Math.Ceiling(filteredCharacters.Count / (double)PageSize)}";
                    btnPrev.Enabled = false;
                    btnNext.Enabled = filteredCharacters.Count > PageSize;
                }

                UpdatePaginationControls(filteredCharacters.Count, 1, PageSize);
            }
        }

        public string GetDescription(object descriptionObj)
        {
            string desc = descriptionObj as string;
            return string.IsNullOrEmpty(desc) ? "No description available " : desc;
        }

        public string GetImageUrl(object thumbnailObj)
        {
            if (thumbnailObj is Thumbnail thumbnail)
            {
                return $"{thumbnail.Path}.{thumbnail.Extension}";
            }
            return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJX5tjwXmVgD4Ax1lD3-4lQ64hAAkPuLt8bw&s";
        }
    }
}