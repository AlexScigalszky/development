var fs = require('fs');
var path = require('path');
var async = require('async');

var folder = './data';

// const maxFileSize of 100MB
var maxFileSize = 100000000;

var save = function (data, cb) {
    var file = path.join(folder, data.fileName);
    var data = JSON.stringify(data.data);
    var countParts = Math.ceil(data.length / maxFileSize);
    console.log({ countParts });
    var partSize = Math.ceil(data.length / countParts);
    console.log({ partSize });
    var parts = [];
    for (var i = 0; i < countParts; i++) {
        parts.push({
            start: i * partSize,
            end: (i + 1) * partSize
        });
    }
    async.map(parts, function (part, cb) {
        var start = part.start;
        var end = part.end;
        var partData = data.substring(start, end);
        var partFile = file + '.' + parts.indexOf(part);
        fs.writeFile(partFile, partData, function (err) {
            console.log('saving partFile: ' + partFile);
            if (err) {
                return cb(err);
            }
            cb(null, partFile);
        });
    }, function (err, results) {
        if (err) {
            return cb(err);
        }
        cb(null, results);
    });
};

// funtion that return the array from multiple files that are starting with the same name
var read = function (fileName, cb) {
    var file = path.join(folder, fileName);
    var parts = [];
    var countParts = fs.readdirSync(folder).filter(function (file) {
        return file.indexOf(fileName) === 0;
    }).length;
    for (var i = 0; i < countParts; i++) {
        parts.push(file + '.' + i);
    }
    async.map(parts, function (part, cb) {
        fs.readFile(part, function (err, data) {
            if (err) {
                return cb(err);
            }
            cb(null, data);
        });
    }, function (err, results) {
        if (err) {
            return cb(err);
        }
        var data = results.join('');
        cb(null, JSON.parse(data));
    });
};

module.exports = {
    save: save,
    read: read
};

/*
 * Test Section 
 */
var testData = {
    fileName: 'test',
    data:
        Array(10000000)
            .fill(0)
            .map((item, index) => ({
                name: 'test' + index,
                age: index
            }))
}

// test save
save(testData, function (err, data) {
    console.log('save test', data);
});

// test read
read('test', function (err, data) {
    console.log('read test', data.length);
});
