using System;

using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class ListViewWithHeaderPage : ContentPage
	{
		public ListViewWithHeaderPage()
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

