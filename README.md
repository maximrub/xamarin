# Introduction
Xamarin provides a way to quickly build native apps for iOS, Android, and Windows, completely in C#.

Read more about the platform at [Xamarin](https://www.xamarin.com).

# Providers
The purpose of this project is to provide you with common packages to help you to get started with [Xamarin](https://www.xamarin.com).

# Getting Started
## Install the Nuget Package
* Xamarin.Extensions.Logging.MobileCenter - Visual Studio Mobile Center logger provider implementation for Microsoft.Extensions.Logging [![NuGet Badge](https://buildstats.info/nuget/Xamarin.Extensions.Logging.MobileCenter?includePreReleases=true)](https://www.nuget.org/packages/Xamarin.Extensions.Logging.MobileCenter)
* Xamarin.Extensions.Configuration.FileStorageJson - Xamarin FileStorage Json configuration provider implementation for Microsoft.Extensions.Configuration [![NuGet Badge](https://buildstats.info/nuget/Xamarin.Extensions.Configuration.FileStorageJson?includePreReleases=true)](https://www.nuget.org/packages/Xamarin.Extensions.Configuration.FileStorageJson)
* Xamarin.FileStorage.Android - Xamarin FileStorage Android provider [![NuGet Badge](https://buildstats.info/nuget/Xamarin.FileStorage.Android?includePreReleases=true)](https://www.nuget.org/packages/Xamarin.FileStorage.Android)
* Xamarin.FileStorage.iOS - Xamarin FileStorage iOS provider [![NuGet Badge](https://buildstats.info/nuget/Xamarin.FileStorage.iOS?includePreReleases=true)](https://www.nuget.org/packages/Xamarin.FileStorage.iOS)
## Configuration file
### Shared PCL
* In your portable library create {FILE_NAME}.json, Build Action as Content and Copy Always.
### iOS
* In the project root, right click and Add > Existing Item
* Go to your PCL folder and find the JSON file.
* Though instead of pressing add, press the drop down, next to the Add button, and Add as Link.
### Android
* In the Assets folder, right click and Add > Existing Item
* Go to your PCL folder and find the JSON file.
* Though instead of pressing add, press the drop down, next to the Add button, and Add as Link.
## Sample app
[Friends App](../../tree/master/Sample) - A basic sample app to help you get started.

**Note:** The sample app includes an example on how to use the [ExrinProviders](https://github.com/maximrub/ExrinProviders) packages.
