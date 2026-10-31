using System.Text;
using LAB2.Encryption;

namespace LAB2;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnPickFileClicked(object sender, EventArgs e)
	{
		var options = new PickOptions
		{
			PickerTitle = "Выберите файл!"
		};

		var result = await PickAndShow(options);
		if(result != null)
		{
			FileName.Text = $"Выбранный файл {result.FileName}";
            FileName.TextColor = Colors.Green;
		}
	}

	public async Task<FileResult> PickAndShow(PickOptions options)
	{		
    	try
    	{
        	var result = await FilePicker.Default.PickAsync(options);
        	if (result != null)
        	{
            	using var stream = await result.OpenReadAsync();
        	}
        	return result;
    	}
    	catch (Exception ex)
		{
			await DisplayAlert("Ошибка", $"Произошел сбой\n{ex.Message}", "OK");
		}

    return null;
	}

	//Генерация ключа шифрования
	private async void OnGenerateKey(object sender, EventArgs e)
	{
		string key = KeyEntry.Text;

		//Переводим текст в UTF-8
		byte[] bytes = Encoding.UTF8.GetBytes(key);
	}
}