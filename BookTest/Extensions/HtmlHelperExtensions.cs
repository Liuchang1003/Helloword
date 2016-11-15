using System.Web;
using System.Web.Mvc;
using BookTest.Models;
using Newtonsoft.Json;

namespace BookTest.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return new HtmlString(JsonConvert.SerializeObject(model, settings));
        }

        #region 创建排序链接
        /// <summary>
        /// 创建排序链接
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="actionName">action</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="queryOptions">排序参数实体</param>
        public static HtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName,
            string sortField, QueryOptions queryOptions)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var isCurrentSortField = queryOptions.SortField == sortField;
            return new MvcHtmlString(string.Format("<a href='{0}'>{1} {2}</a>",
                urlHelper.Action(actionName, new
                {
                    SortField = sortField,
                    SortOrder = (isCurrentSortField && queryOptions.SortOrder == SortOrderMolds.ASC)
                    ? SortOrderMolds.DESC : SortOrderMolds.ASC
                }), fieldName, BuildSortIcon(isCurrentSortField, queryOptions)
                ));
        }

        private static string BuildSortIcon(bool isCurrentSOrtField, QueryOptions queryOptions)
        {
            string sortIcon = "sort";

            if (isCurrentSOrtField)
            {
                sortIcon += "-by-attributes";
                if (queryOptions.SortOrder == SortOrderMolds.DESC)
                    sortIcon += "-alt";
            }
            return string.Format("<span class='{0} {1}{2}'></span>", "glyphicon", "glyphicon-", sortIcon);
        }
        #endregion

        #region 创建上一页下一页按钮

        public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions,
            string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            return new MvcHtmlString(string.Format(
                "<nav>" +
                "<ul class='pager'>" +
                ((queryOptions.Page != 1) ? "<li class='previous {0}'>{1}</li>" : string.Empty) +
                ((queryOptions.Page != queryOptions.TotalPage) ? "<li class='next {2}'>{3}</li>" : string.Empty) +
                "</ul>" +
                "</nav>",
                IsPreviousDisable(queryOptions),
                BuildPreviousLink(urlHelper, queryOptions, actionName),
                IsNextDisable(queryOptions),
                BuildNextLink(urlHelper, queryOptions, actionName)));
        }

        private static string IsPreviousDisable(QueryOptions queryOptions)
        {
            return (queryOptions.Page == 1) ? "disabled" : string.Empty;
        }

        private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format("<a href='{0}'><span aria-hidden='true'>&larr;</span>Previous</a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    Page = queryOptions.Page - 1,
                    PageSize = queryOptions.PageSize
                }));
        }

        private static string IsNextDisable(QueryOptions queryOptions)
        {
            return (queryOptions.Page == queryOptions.TotalPage) ? "disabled" : string.Empty;
        }
        private static string BuildNextLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
        {
            return string.Format("<a href='{0}'>Next<span aria-hidden='true'>&rarr;</span></a>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    Page = queryOptions.Page + 1,
                    PageSize = queryOptions.PageSize
                }));
        }

        #endregion
    }
}