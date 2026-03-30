hmLoadTopic({
hmKeywords:"",
hmTitle:"ksDistancePntPntOnCurve - пример использования",
hmDescription:"double x, y; if (::Cursor(NULL, &x, &y, NULL)) { \/\/ указываем кривую reference curve = ::FindObj(x, y, 1000); \/\/ найти ближайшую кривую double x1, y1; if (::ExistObj(curve) &",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksDistancePntPntOnCurve - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1718518\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x, y;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::Cursor(NULL, &amp;x, &amp;y, NULL)) { \/\/ указываем кривую<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference curve = ::FindObj(x, y, 1000); \/\/ найти ближайшую кривую<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x1, y1;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::ExistObj(curve) &amp;&amp; ::Cursor(NULL, &amp;x, &amp;y, NULL) \/\/ проверим, существует ли кривая<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&amp;&amp; ::Cursor(NULL, &amp;x1, &amp;y1, NULL)) { \/\/ и укажем две точки на кривой<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double len = ::ksDistancePntPntOnCurve(curve, x, y, x1, y1); \/\/ расстояние между точками<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">char buf[128];<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::sprintf(buf, &quot;Расстояние между точками = %g&quot;, len);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Message(buf); \/\/ выведем результат<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
