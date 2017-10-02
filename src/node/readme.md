# Node Not Hot Dog
1. Create a new Function App using the 'HTTP Trigger - Javascript' template
2. Copy the contents of index.js to the index.js file in the function
3. Create a new package.json file
4. Go to the 'Platform Settings' section of the function app configuration
![Open Platform Settings](/images/platform-settings.png)
5. Select 'Console'
6. In the console window cd to your function app and run npm install to install the packages
```
npm install
```
7. Under platform settings, select 'Application Settings' and add the following application setting key and value
```
key: Vision_API_Subscription_Key
value: <Insert your vision API key>
```
8. Back at the function page enter an image URL in the 'Request Body' and run a test.
![Open Platform Settings](/images/test-pane.png)
