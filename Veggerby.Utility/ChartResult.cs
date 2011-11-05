//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;
//using System.Web.UI.DataVisualization.Charting;

//namespace Danfoss.Treasury.Web.Core
//{
//    public class ChartResult : FileResult
//    {
//        public ChartResult(string fileName, Chart chart)
//            : base("image/png")
//        {
//            this._Chart = chart;
//            this.FileDownloadName = fileName;
//        }

//        private readonly Chart _Chart;

//        protected override void WriteFile(HttpResponseBase response)
//        {
//            using (var ms = new MemoryStream())
//            {
//                this._Chart.SaveImage(ms, ChartImageFormat.Png);
//                ms.Seek(0, SeekOrigin.Begin);
//                var buffer = new byte[ms.Length];
//                ms.Read(buffer, 0, (int)ms.Length);
//                response.OutputStream.Write(buffer, 0, (int)ms.Length);
//            }
//        }
//    }
//}