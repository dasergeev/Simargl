hmLoadTopic({
hmKeywords:"",
hmTitle:"NurbsForConicCurve - пример использования",
hmDescription:"\/\/ построить дугу эллипса (параметры эллипса: центр - 0,0, a = 20, b= 10);",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">NurbsForConicCurve - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1720777\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ построить дугу эллипса (параметры эллипса: центр - 0,0, a = 20, b= 10);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x[4], y[4];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">x[0] = -19.3202; y[0] = 2.5850; \/\/ начальная точка эллиптической дуги (1)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">x[1] = -10.0; y[1] = 20.0; \/\/ пересечение касательных к дуге из точкек 1 и 2<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">x[2] = 14.6144; y[2] = 6.8268; \/\/ конечная точка эллиптической дуги (2)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">x[3] = 0.0; y[3] = 10.0; \/\/ точка на дуге<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference p = NurbsForConicCurve (x, y, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (p)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">{<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj (p, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Message (&quot;Эллиптическая дуга построена&quot;);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">LightObj(p, 0);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">else<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Error (&quot;Неверно заданы характерные точки&quot;);<\/span><\/p>\n\r"
})
