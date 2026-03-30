hmLoadTopic({
hmKeywords:"",
hmTitle:"ksPointsOnCurveByStep - Пример использования",
hmDescription:"double x = 100, y = 100, step = 10.5; \/\/ указанная кривая reference pObj = ::FindObj(x, y, 1000);   if (::ExistObj(pObj)) { \/\/ проверим существование объекта   reference ar",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksPointsOnCurveByStep - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1719611\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">double x = 100, y = 100, step = 10.5;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ указанная кривая<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference pObj = ::FindObj(x, y, 1000);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::ExistObj(pObj)) { \/\/ проверим существование объекта<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">reference arr = ::ksPointsOnCurveByStep(pObj, step); \/\/ массив точек<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">uint count = ::GetArrayCount(arr); \/\/ количество полученных точек<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">MathPointParam point;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/ нарисуем точки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">for (uint i = 0; i &lt; count; i++)<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">if (::GetArrayItem(arr, i, &amp;point, sizeof(MathPointParam)))<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::Point(point.x, point.y, 4);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">}<\/span><\/p>\n\r"
})
