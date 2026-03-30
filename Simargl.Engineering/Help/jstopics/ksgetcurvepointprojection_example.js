hmLoadTopic({
hmKeywords:"",
hmTitle:"ksGetCurvePointProjection - пример использования",
hmDescription:"reference curve = ::LineSeg (300, 100, -400, -200, 1);",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksGetCurvePointProjection - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1718503\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference curve = ::LineSeg (300, 100, -400, -200, 1);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x = 100;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double y = 50;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksGetCurvePointProjection (curve, x, y, &amp;x, &amp;y); \/\/ проецируем точку на кривую<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::sprintf (buf, &quot;координаты проекции точки на кривой: (%g, %g)&quot;, x, y);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf);<\/span><\/p>\n\r"
})
