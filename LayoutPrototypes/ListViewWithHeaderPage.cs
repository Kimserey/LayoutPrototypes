using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class StoreHeaderTemplate : ViewCell
	{
		public StoreHeaderTemplate()
		{

		}
	}

	public class ListViewWithHeaderPage : ContentPage
	{
		public ListViewWithHeaderPage()
		{
			var list = new ListView(ListViewCachingStrategy.RecycleElement);
			list.ItemsSource = 
				new List<string> { 
					"hey", 
					"ho", 
					"hi", 
					"eee",
					"hey", 
					"ho", 
					"hi", 
					"eee",
					"hey",
					"ho",
					"hi",
					"eee",
					"hey",
					"ho",
					"hi",
					"eee",
					"hey",
					"ho",
					"hi",
					"eee",
					"hey",
					"ho",
					"hi",
					"eee"
				};

			var headerGrid = new Grid { ColumnSpacing = 5, HeightRequest = 80 };
			headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
			headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
			headerGrid.ColumnDefinitions.Add(new ColumnDefinition());
			headerGrid.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 0, 0);
			headerGrid.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 1, 0);
			headerGrid.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 2, 0);

			var header = 
				new StackLayout { 
					Padding = 5,
					Children = {
						new Label { Text = "Quick access (with most recent added baskets)", FontSize = 10, FontAttributes = FontAttributes.Bold },
						headerGrid,
						new Label { Text = "All stores (alphabetical order)", FontSize = 10, FontAttributes = FontAttributes.Bold }
					}
				};
			list.hea
			list.Header = header;

			Content = new StackLayout
			{
				Children = {
					list
				}
			};
		}
	}
}

