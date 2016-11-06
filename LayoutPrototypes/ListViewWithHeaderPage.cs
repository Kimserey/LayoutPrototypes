using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LayoutPrototypes
{
	public class HeaderViewModel : INotifyPropertyChanged
	{
		private QuickAccess quickAccess1;

		public event PropertyChangedEventHandler PropertyChanged;

		public QuickAccess QuickAccess1
		{
			get { return quickAccess1; }
			set {
				quickAccess1 = value;
				OnPropertyChanged();
			}
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public HeaderViewModel()
		{
			this.QuickAccess1 = new QuickAccess { Label = "Test", Color = Color.Red };
		}
	}


	public class QuickAccess
	{
		public string Label { get; set; }
		public Color Color { get; set; } 
	}

	public class StoreHeaderTemplate : Grid
	{
		public Label QuickAccess1Name { get; set; }
		public BoxView QuickAccess1Box { get; set; }

		public QuickAccess QuickAccess1 { 
			get { return (QuickAccess)GetValue(QuickAccess1Property); }
			set { SetValue(QuickAccess1Property, value); }
		}

		public static BindableProperty QuickAccess1Property =
			BindableProperty.Create(
				nameof(QuickAccess1),
				typeof(QuickAccess),
				typeof(StoreHeaderTemplate),
				null, 
				propertyChanged: (bindable, oldValue, newValue) => {
					if (newValue != null)
					{
						var elt = (StoreHeaderTemplate)bindable;
						var quickAccess = (QuickAccess)newValue;
						elt.QuickAccess1Name.Text = quickAccess.Label;
						elt.QuickAccess1Box.Color = quickAccess.Color;
					}
				});

		public StoreHeaderTemplate()
		{
			QuickAccess1Box = new BoxView();
			QuickAccess1Name = new Label { FontSize = 9, BackgroundColor = Color.Blue.MultiplyAlpha(0.5), VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };

			this.Margin = 5;
			this.ColumnSpacing = 5;
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.ColumnDefinitions.Add(new ColumnDefinition());
			this.RowDefinitions.Add(new RowDefinition());
			this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
			this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
			this.RowDefinitions.Add(new RowDefinition());
			this.Children.Add(new Label { Text = "Quick access (with most recent added baskets)", FontSize = 10, FontAttributes = FontAttributes.Bold }, 0, 3, 0, 1);
			this.Children.Add(QuickAccess1Box, 0, 1, 1, 3);
			this.Children.Add(QuickAccess1Name, 0, 1, 2, 3);
			this.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 1, 2, 1, 3);
			this.Children.Add(new BoxView { BackgroundColor = Color.Blue }, 2, 3, 1, 3);
			this.Children.Add(new Label { Text = "All stores (alphabetical order)", FontSize = 10, FontAttributes = FontAttributes.Bold }, 0, 3, 3, 4);
		}
	}

	public class ListViewWithHeaderPage : ContentPage
	{
		public ListViewWithHeaderPage()
		{
			var list = new ListView(ListViewCachingStrategy.RecycleElement);
			list.ItemsSource = Enumerable.Range(0, 20).Select(arg => "Something " + arg).ToList();

			list.Header = new HeaderViewModel();

			// Had to set the binding within the datatemplate.
			// Not sure why but the template IS NOT a ViewCell.
			// Passing a ViewCell will crash the header with an incorrect value error.
			list.HeaderTemplate = new DataTemplate(() => {
				var template = new StoreHeaderTemplate();
				template.SetBinding(StoreHeaderTemplate.QuickAccess1Property, "QuickAccess1");
				return template;
			});

			var button = new Button { Text = "Make blue" };
			button.Clicked += (sender, e) => {
				((HeaderViewModel)list.Header).QuickAccess1 = new QuickAccess { Color = Color.Blue, Label = "It's blue now" };
			};
			list.Footer = button;

			Content = new StackLayout
			{
				Children = {
					list
				}
			};
		}
	}
}

