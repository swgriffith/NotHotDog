# Node Not Hot Dog
First you'll need to create a new Computer Vision API instance in the Azure portal by clicking '+' and searching for Vision. Once provisioned just go to the instance and copy the URL from the 'Overview' page and one of the keys from the 'Keys' tab.

1. Create a new Function App using the 'HTTP Trigger - Javascript' template
2. Copy the contents of index.js to the index.js file in the function
3. Create a new package.json file and paste the contents of package.json from this repo

![][https://github.com/swgriffith/NotHotDog/raw/master/images/viewfiles.png]

4. Go to the 'Platform features' section of the function app configuration (The lightening bolt at the top of the functions list)

![][https://github.com/swgriffith/NotHotDog/raw/master/images/platformfeatures.png]

5. Select 'Console'
6. In the console window cd to your function app and run npm install to install the packages
```
npm install
```
7. Under platform features, select 'Application Settings' and add the following application setting key and value
```
key: Vision_API_URL
value: <Insert your vision API URL>

key: Vision_API_Subscription_Key
value: <Insert your vision API key>
```
8. Back at the function page enter an image URL in the 'Request Body' and run a test.
