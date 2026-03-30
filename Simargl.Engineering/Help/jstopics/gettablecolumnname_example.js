hmLoadTopic({
hmKeywords:"",
hmTitle:"GetTableName, GetColumnName - пример использования",
hmDescription:"char buf[128],nameOBDC[128]; \/\/имя источника данных",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">GetTableName, GetColumnName - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1732253\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128],nameOBDC[128]; \/\/имя источника данных<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/создать объект, обслуживающий базу данных<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference bd = CreateDB (&quot;ODBC_DB&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (ConnectDB (bd, nameOBDC)) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (GetTableName (bd, buf, 128, \'F\')) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">do {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (GetColumnName (bd, buf, buf, 128, \'F\')) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">do {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message(buf);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">} while (GetColumnName (bd, buf, buf, 128, \'N\'));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">} while (GetTableName (bd, buf, 128, \'N\')) ;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
