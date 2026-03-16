# RemoteShutdown-App
A simple android app to edit a dropbox file's value from 0 to 1 remotely via a toggle.

> [!NOTE]
> This app is best paired with the [RemoteShutdown Windows service](https://github.com/yousif51811/RemoteShutdown)

<img src="/Demo.jpg" alt="Demo" width="400"/>

## Setup
#### 1. Create a dropbox app
Head to [The dropbox developer dashboard](www.dropbox.com/developers/apps) and create a new scoped app.

Change your app's permission to allow `files.content.write`

In the newly created folder in your dropbox account under `Apps\MyApp` create a sh.txt - This will later be the file where the remote value is stored.

#### 2. Note down your URL and access key
In your app's dashboard - Generate a new acces token and Note it down

In your apps folder `Apps\MyApp` Get a new link to your file, Make sure to change the `?dl=0` to `dl=1` at the end of the URL.

## Building
#### 1. Ensure you have the .NET SDK installed.
#### 2. Clone this repository
```
git clone https://github.com/yousif51811/RemoteShutdown-App.git
```
#### 3. Publish the app
```
dotnet publish -f net10.0-android -c Release
```
#### 4. An APK will be available at `\bin\Releasenet\10.0-android\publish`

-----------
Made with ❤️ by yousif51811