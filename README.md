
- To run tests with the TRX output:
`dotnet test --configuration Prod --logger "trx;LogFileName=TestResults.trx"`


- To generate LivingDoc+ report:
`livingdoc feature-folder "Tests" -t "Tests\bin\Prod\net8.0\TestExecution.json" -o "LivingDoc.html"`


- To incorporate script to the LivingDoc+ report (tag replacement):
`.\AddScriptsToHtml.ps1 LivingDoc.html ReplaceTags.js`


- To generate xlsx report:
```
cd Utils\XlsxReportGenerator\
dotnet run [inputFileName.trx] [outputFileName.xlsx]
```

- To freeze actions in the browser (if you can not locate an element in case it is hides when you switch to the DevTools):

1. Open Dev Tools (Ctrl+Shift+I)
2. Switch to the Console tab
3. Try to paste smth (and see the error)
4. Type "allow pasting"
**_Attention! This command could be in the language of the Chrome localization_**
5. Paste the next code:
```
var observer = new MutationObserver(function(mutations) {
	mutations.forEach(function(mutation) {
            if (mutation.addedNodes.length) {
              console.log('Added nodes:', mutation.addedNodes);
              debugger;  // This will pause script execution
            }
          });
        });

observer.observe(document.body, { childList: true, subtree: true });
```
6. Press Enter
7. Switch to the Elements tab
8. Use the "play" button to go to the next DOM change/action

- To close all opened webdrivers:
`taskkill /F /IM "chromedriver.exe" /T`
