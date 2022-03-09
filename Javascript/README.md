# json-to-multiples-files.js
## There are two funtions: 

### save(data, callback)
Save an array in multiples files acoordin to given filesize
### read(fileName, callback)
Read multiples files with arrays (fragmented) and return it as a single array with all data

## How to use

```javascript
var testData = {
    fileName: 'test',
    data: Array(10000000).fill(0).map((item, index) => index)
}

save(testData, function (err, data) {
    console.log('save test', data);
});

read('test', function (err, data) {
    console.log('read test', data.length);
});

```
