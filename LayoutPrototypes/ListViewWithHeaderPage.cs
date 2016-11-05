using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LayoutPrototypes
{
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

			var header = 
				new StackLayout { 
					Padding = 5,
					Children = { 
						new Label { Text = "Quick access" },
						new StackLayout { 
							Orientation = StackOrientation.Horizontal,
							Spacing = 2,
							Children = {
								new BoxView { BackgroundColor = Color.Blue },
								new BoxView { BackgroundColor = Color.Blue },
								new BoxView { BackgroundColor = Color.Blue },
							}
						},
						new Label { Text = "Stores" }
					}
				};

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

