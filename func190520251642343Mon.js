// JavaScript source code
//for console
var console = {};
console.log = function (text) {
    external.log(text)
}
console.info = function (text) {
    external.info(text)
}
console.warn = function (text) {
    external.warn(text)
}
console.error = function (text) {
    external.log("ERR: " + text)
}
console.clear = function () {
    external.log("DEL19may2025164624282930")
}
console.debug = function (text) {
    external.log("DEB19may2025164624282930" + text)
}
//other stuff