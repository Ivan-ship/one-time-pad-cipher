using System.Text;
using LAB2.Encryption;

namespace LAB2;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private EncryptionKey? encryptionKey;
	private long? fileSize;

	//Загрузка файла!
	private async void OnPickFileClicked(object sender, EventArgs e)
	{
		var options = new PickOptions
		{
			PickerTitle = "Выберите файл!"
		};

		var result = await PickAndShow(options);
		if(result != null)
		{
			using(var stream = await result.OpenReadAsync())
			{
				long bytesCount = stream.Length;
				fileSize = bytesCount;
				FileName.Text = $"Выбранный файл {result.FileName}\n Размер текста {fileSize} байт";
            	FileName.TextColor = Colors.Green;
			}
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

		if(!int.TryParse(key, out int keySize))
		{
			await DisplayAlert("Ошибка!", "Введите конкретное число!", "OK");
			return;
		}

		if(keySize < 3)
		{
			await DisplayAlert(
				"Ошибка!", 
				"Размер ключа не должен быть менее 3 символов!", 
				"OK");
			return;
		}
		encryptionKey = new EncryptionKey(keySize);
		await DisplayAlert("Успех!", "Успешное создание ключа!", "OK");
	}

	//Сохранение ключа шифрования
	private async void OnSaveKeyCliccked(object sender, EventArgs e)
	{
		if(encryptionKey == null)
		{
			await DisplayAlert(
				"Ошибка!", 
				"Сначала нужно создать ключ шифрований!", 
				"OK");
			return;
		}

		try
		{
			await encryptionKey.EncryptArray();
			await DisplayAlert(
				"Успех!", 
				"Успешное сохранение!", 
				"OK");
		}
		catch(Exception ex)
		{
			await DisplayAlert(
				"Ошибка!", 
				$"{ex}", 
				"OK");
		}
	}

	private async void OnDowloadKey(object sender, EventArgs e)
	{
		var options = new PickOptions
		{
			PickerTitle = "Выберите файл!"
		};

		var result = await PickAndShow(options);
		if(result != null)
		{
			using(var stream = await result.OpenReadAsync())
			{
				long bytssCount = stream.Length;
				FileName.Text = $"Ключ шифрования {bytssCount} байт";
            	FileName.TextColor = Colors.Green;
			}
		}
	}
}