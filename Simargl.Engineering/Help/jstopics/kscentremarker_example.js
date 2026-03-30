hmLoadTopic({
hmKeywords:"",
hmTitle:"ksCentreMarker - пример использования",
hmDescription:"CentreParam cPar; \/\/ структура параметров объекта \"обозначение центра\"",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksCentreMarker - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1724345\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">CentreParam cPar; \/\/ структура параметров объекта &quot;обозначение центра&quot;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::memset(&amp;cPar, 0, sizeof(cPar));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.x = 50; \/\/ точка привязки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.y = 50;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.type = 2; \/\/ тип обозначения центра - две оси<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.lenXpTail = 40; \/\/ длина &quot;хвостиков&quot;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.lenXmTail = 60;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.lenYpTail = 20;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">cPar.lenYmTail = 45;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksCentreMarker(&amp;cPar); \/\/ создать объект &quot;обозначение центра&quot;<\/span><\/p>\n\r"
})
