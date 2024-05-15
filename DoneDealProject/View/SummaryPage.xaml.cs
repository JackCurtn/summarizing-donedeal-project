using DoneDealProject.ViewModel;

namespace DoneDealProject.View;

public partial class SummaryPage : ContentPage
{
	public SummaryPage(SummaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}