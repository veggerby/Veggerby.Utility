using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Veggerby.Utility.Mvc.Paging
{
    public class Pager
    {
        private readonly ViewContext _ViewContext;
        private readonly int _PageSize;
        private readonly int _CurrentPage;
        private readonly int _TotalItemCount;
        private readonly RouteValueDictionary _LinkWithoutPageValuesDictionary;

        public Pager(ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
        {
            this._ViewContext = viewContext;
            this._PageSize = pageSize;
            this._CurrentPage = currentPage;
            this._TotalItemCount = totalItemCount;
            this._LinkWithoutPageValuesDictionary = valuesDictionary;
        }

        public string RenderHtml()
        {
            var pageCount = (int)Math.Ceiling(this._TotalItemCount / (double)this._PageSize);
            if (pageCount <= 1)
            {
                return string.Empty;
            }

            const int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            // Previous
            if ((this._CurrentPage > 1) && (pageCount > 1))
            {
                sb.Append(GeneratePageLink("prev", this._CurrentPage - 1));
            }

            int start = 1;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this._CurrentPage - middle);
                int above = (this._CurrentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("<span class=\"dots\">&hellip;</span>");
            }

            for (int i = start; i <= end; i++)
            {
                if (i == this._CurrentPage)
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(), i));
                }
            }

            if (end < (pageCount - 3))
            {
                sb.Append("&hellip;");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1));
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
            }

            // Next
            if ((this._CurrentPage < pageCount) && (pageCount > 1))
            {
                sb.Append(GeneratePageLink("next", (this._CurrentPage + 1)));
            }

            return sb.ToString();
        }

        private string GeneratePageLink(string linkText, int pageNumber)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this._LinkWithoutPageValuesDictionary)
                                              {{"p", pageNumber}};

            var virtualPathData = RouteTable.Routes.GetVirtualPath(this._ViewContext.RequestContext, pageLinkValueDictionary);

            if (virtualPathData != null)
            {
                return String.Format("<a href=\"{0}\"><span>{1}</span></a>", virtualPathData.VirtualPath, linkText);
            }
            
            return null;
        }
    }
}