hmLoadTopic({
hmKeywords:"",
hmTitle:"ksColouringEx - Пример использования",
hmDescription:"double x = 0; double y = 0; \/\/ Указать точку в замкнутой области if ( Cursor( NULL, &x, &y, NULL ) ) { \/\/ Создать контур из объектов окружающих данную точку reference group =",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksColouringEx - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1720079\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x = 0; double y = 0;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Указать точку в замкнутой области<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if ( Cursor( NULL, &amp;x, &amp;y, NULL ) ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Создать контур из объектов окружающих данную точку<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference group = ksMakeEncloseContours( 0, x, y );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if ( group ) {<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Создать заливку для группы<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksColouringEx( RGB(255, 0, 0), group );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ Удалить временную группу<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">DeleteObj( group );<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
