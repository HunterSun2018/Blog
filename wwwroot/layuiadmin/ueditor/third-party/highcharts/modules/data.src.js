/** layuiAdmin.pro-v1.2.1 LPPL License By http://www.layui.com/admin/ */
 ;!function(t){var e=t.each,n=function(t,e){this.init(t,e)};t.extend(n.prototype,{init:function(t,e){this.options=t,this.chartOptions=e,this.columns=t.columns||this.rowsToColumns(t.rows)||[],this.columns.length?this.dataFound():(this.parseCSV(),this.parseTable(),this.parseGoogleSpreadsheet())},getColumnDistribution:function(){var n=this.chartOptions,i=function(e){return(t.seriesTypes[e||"line"].prototype.pointArrayMap||[0]).length},s=n&&n.chart&&n.chart.type,o=[];e(n&&n.series||[],function(t){o.push(i(t.type||s))}),this.valueCount={global:i(s),individual:o}},dataFound:function(){this.parseTypes(),this.findHeaderRow(),this.parsed(),this.complete()},parseCSV:function(){var t,n=this,i=this.options,s=i.csv,o=this.columns,r=i.startRow||0,a=i.endRow||Number.MAX_VALUE,l=i.startColumn||0,u=i.endColumn||Number.MAX_VALUE,h=0;s&&(t=s.replace(/\r\n/g,"\n").replace(/\r/g,"\n").split(i.lineDelimiter||"\n"),e(t,function(t,s){var c,p=n.trim(t),m=0===p.indexOf("#"),d=""===p;s>=r&&s<=a&&!m&&!d&&(c=t.split(i.itemDelimiter||","),e(c,function(t,e){e>=l&&e<=u&&(o[e-l]||(o[e-l]=[]),o[e-l][h]=t)}),h+=1)}),this.dataFound())},parseTable:function(){var t,n=this.options,i=n.table,s=this.columns,o=n.startRow||0,r=n.endRow||Number.MAX_VALUE,a=n.startColumn||0,l=n.endColumn||Number.MAX_VALUE;i&&("string"==typeof i&&(i=document.getElementById(i)),e(i.getElementsByTagName("tr"),function(n,i){t=0,i>=o&&i<=r&&e(n.childNodes,function(e){("TD"===e.tagName||"TH"===e.tagName)&&t>=a&&t<=l&&(s[t]||(s[t]=[]),s[t][i-o]=e.innerHTML,t+=1)})}),this.dataFound())},parseGoogleSpreadsheet:function(){var t,e,n=this,i=this.options,s=i.googleSpreadsheetKey,o=this.columns,r=i.startRow||0,a=i.endRow||Number.MAX_VALUE,l=i.startColumn||0,u=i.endColumn||Number.MAX_VALUE;s&&jQuery.getJSON("https://spreadsheets.google.com/feeds/cells/"+s+"/"+(i.googleSpreadsheetWorksheet||"od6")+"/public/values?alt=json-in-script&callback=?",function(i){var s,h,c=i.feed.entry,p=c.length,m=0,d=0;for(h=0;h<p;h++)s=c[h],m=Math.max(m,s.gs$cell.col),d=Math.max(d,s.gs$cell.row);for(h=0;h<m;h++)h>=l&&h<=u&&(o[h-l]=[],o[h-l].length=Math.min(d,a-r));for(h=0;h<p;h++)s=c[h],t=s.gs$cell.row-1,e=s.gs$cell.col-1,e>=l&&e<=u&&t>=r&&t<=a&&(o[e-l][t-r]=s.content.$t);n.dataFound()})},findHeaderRow:function(){var t=0;e(this.columns,function(e){"string"!=typeof e[0]&&(t=null)}),this.headerRow=0},trim:function(t){return"string"==typeof t?t.replace(/^\s+|\s+$/g,""):t},parseTypes:function(){for(var t,e,n,i,s,o=this.columns,r=o.length;r--;)for(t=o[r].length;t--;)e=o[r][t],n=parseFloat(e),i=this.trim(e),i==n?(o[r][t]=n,n>31536e6?o[r].isDatetime=!0:o[r].isNumeric=!0):(s=this.parseDate(e),0!==r||"number"!=typeof s||isNaN(s)?o[r][t]=""===i?null:i:(o[r][t]=s,o[r].isDatetime=!0))},dateFormats:{"YYYY-mm-dd":{regex:"^([0-9]{4})-([0-9]{2})-([0-9]{2})$",parser:function(t){return Date.UTC(+t[1],t[2]-1,+t[3])}}},parseDate:function(t){var e,n,i,s,o=this.options.parseDate;if(o&&(e=o(t)),"string"==typeof t)for(n in this.dateFormats)i=this.dateFormats[n],s=t.match(i.regex),s&&(e=i.parser(s));return e},rowsToColumns:function(t){var e,n,i,s,o;if(t)for(o=[],n=t.length,e=0;e<n;e++)for(s=t[e].length,i=0;i<s;i++)o[i]||(o[i]=[]),o[i][e]=t[e][i];return o},parsed:function(){this.options.parsed&&this.options.parsed.call(this,this.columns)},complete:function(){var e,n,i,s,o,r,a,l,u=this.columns,h=this.options;if(h.complete){for(this.getColumnDistribution(),u.length>1&&(e=u.shift(),0===this.headerRow&&e.shift(),e.isDatetime?n="datetime":e.isNumeric||(n="category")),r=0;r<u.length;r++)0===this.headerRow&&(u[r].name=u[r].shift());for(s=[],r=0,l=0;r<u.length;l++){for(i=t.pick(this.valueCount.individual[l],this.valueCount.global),o=[],a=0;a<u[r].length;a++)o[a]=[e[a],void 0!==u[r][a]?u[r][a]:null],i>1&&o[a].push(void 0!==u[r+1][a]?u[r+1][a]:null),i>2&&o[a].push(void 0!==u[r+2][a]?u[r+2][a]:null),i>3&&o[a].push(void 0!==u[r+3][a]?u[r+3][a]:null),i>4&&o[a].push(void 0!==u[r+4][a]?u[r+4][a]:null);s[l]={name:u[r].name,data:o},r+=i}h.complete({xAxis:{type:n},series:s})}}}),t.Data=n,t.data=function(t,e){return new n(t,e)},t.wrap(t.Chart.prototype,"init",function(n,i,s){var o=this;i&&i.data?t.data(t.extend(i.data,{complete:function(r){i.series&&e(i.series,function(e,n){i.series[n]=t.merge(e,r.series[n])}),i=t.merge(r,i),n.call(o,i,s)}}),i):n.call(o,i,s)})}(Highcharts);