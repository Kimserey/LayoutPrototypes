using System;

using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class RelativeLayoutTest : ContentPage
	{
		public RelativeLayoutTest()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


