using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPProject.Models.Home;

namespace ASPProject.Helpers
{
	public static class ItemExtensions
	{
		public static IHtmlString AddItemsCSS(this HtmlHelper helper)
        {
			string html =
				"<style>" +
				"</style>";

			return new MvcHtmlString(html);
		}

		public static IHtmlString ShowItem(this HtmlHelper helper, Item item)
		{
			string html =
					"<p data-toggle='modal' data-target='#myModal1' class='offer-img'>" +
						"<img src = '{0}' class='img-responsive item-img' alt=''>" +
					"</p>" +
					"<div class='mid-1'>" +
						"<div class='women'>" +
							"<h6>{1}</h6>" +
						"</div>" +
						"<div class='mid-2'>" +
							"<p ><em class='item_price'>${2}</em></p>" +
							"<div class='block'>" +
								"<div class='starbox small ghosting'> </div>" +
							"</div>" +
							"<div class='clearfix'></div>" +
						"</div>" +
						/*"<div class='add'>" +
							"<button class='button' data-id='{3}' data-name='{1}' data-price='{2}' data-image='{0}'>Add to Cart</button>" +
						"</div>" +*/
					"</div>";

			Random rand = new Random(Guid.NewGuid().GetHashCode());
			string ImagePath1 = "https://cdn.mos.cms.futurecdn.net/VdvpuXPAzBGSYKwznGb4N9-1024-80.jpg.webp";
			string ImagePath2 = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE4OXzi?ver=3a58&q=90&m=6&h=270&w=270&b=%23FF171717&o=f&aim=true";
			string ImagePath3 = "https://media.istockphoto.com/photos/modern-laptop-with-empty-screen-on-white-background-mockup-design-picture-id1182241805?b=1&k=20&m=1182241805&s=170667a&w=0&h=EDTQE8otN4xNEDfj-r0cFIlMmnLKWcQM_xTesSRKSIc=";
			double randVal = rand.NextDouble();
			string selectedImg;
			if (randVal < 0.3) selectedImg = ImagePath1;
			else if (randVal < 0.6) selectedImg = ImagePath2;
			else selectedImg = ImagePath3;


			html = string.Format(html, selectedImg, item.ItemName, item.Price, item.Id);
            return new MvcHtmlString(html);
        }
    }
}