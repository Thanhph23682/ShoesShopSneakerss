!function(t){var r={};function o(e){if(r[e])return r[e].exports;var n=r[e]={i:e,l:!1,exports:{}};return t[e].call(n.exports,n,n.exports,o),n.l=!0,n.exports}o.m=t,o.c=r,o.d=function(e,n,t){o.o(e,n)||Object.defineProperty(e,n,{enumerable:!0,get:t})},o.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},o.t=function(n,e){if(1&e&&(n=o(n)),8&e)return n;if(4&e&&"object"==typeof n&&n&&n.__esModule)return n;var t=Object.create(null);if(o.r(t),Object.defineProperty(t,"default",{enumerable:!0,value:n}),2&e&&"string"!=typeof n)for(var r in n)o.d(t,r,function(e){return n[e]}.bind(null,r));return t},o.n=function(e){var n=e&&e.__esModule?function(){return e.default}:function(){return e};return o.d(n,"a",n),n},o.o=function(e,n){return Object.prototype.hasOwnProperty.call(e,n)},o.p="",o(o.s=13)}({"./app/~/adminassets/es6/pages/icon.js":function(module,exports){eval("class Icon {\r\n\r\n    static init() {\r\n        function copy(value)  {\r\n            const promise = new Promise((resolve) => {\r\n                let copyTextArea = null;\r\n                try {\r\n                    copyTextArea = document.createElement(\"textarea\");\r\n                    copyTextArea.style.height = '0px';\r\n                    copyTextArea.style.opacity = '0';\r\n                    copyTextArea.style.width = '0px';\r\n                    document.body.appendChild(copyTextArea);\r\n                    copyTextArea.value = value;\r\n                    copyTextArea.select();\r\n                    document.execCommand('copy');\r\n                    resolve(value);\r\n                } finally {\r\n                    if (copyTextArea && copyTextArea.parentNode) {\r\n                        copyTextArea.parentNode.removeChild(copyTextArea);\r\n                    }\r\n                }\r\n            });\r\n    \r\n            return (promise);\r\n        }\r\n\r\n        function showToast() {\r\n            var toastHTML = `<div class=\"toast fade hide\" data-delay=\"1500\">\r\n                <div class=\"toast-body\">\r\n                    <i class=\"anticon anticon-check-o text-success\"></i>\r\n                    <span class=\"ml-1\">Copied</span>\r\n                </div>\r\n            </div>`\r\n        \r\n            $('#notification-toast').append(toastHTML)\r\n            $('#notification-toast .toast').toast('show');\r\n            setTimeout(function(){ \r\n                $('#notification-toast .toast:first-child').remove();\r\n            }, 1500);\r\n        }\r\n\r\n        $('.icons-list li').on('click', (e) => {\r\n            const $this = $(e.currentTarget);\r\n            const copiedString = $this.children('.icon-wrap').html()\r\n            copy(copiedString).then(() => {\r\n                showToast()\r\n            });\r\n        })\r\n    }\r\n    \r\n}\r\n\r\n$(() => { Icon.init(); });\r\n\r\n\n\n//# sourceURL=webpack:///./app/~/adminassets/es6/pages/icon.js?")},13:function(module,exports,__webpack_require__){eval('module.exports = __webpack_require__(/*! C:\\Users\\Nate\\Desktop\\themeforest selling\\Enlink-bootstrap\\v1.0.1\\demo\\app\\assets\\es6\\pages\\icon.js */"./app/~/adminassets/es6/pages/icon.js");\n\n\n//# sourceURL=webpack:///multi_./app/~/adminassets/es6/pages/icon.js?')}});