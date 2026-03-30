hmLoadTopic({
hmKeywords:"",
hmTitle:"ksRectangle - Пример использования",
hmDescription:"RectangleParam par; \/\/ структура параметров прямоугольника",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksRectangle - Пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcg1719971\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">RectangleParam par; \/\/ структура параметров прямоугольника<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::memset(&amp;par, 0, sizeof(RectangleParam));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">&nbsp;<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.x = -73.55; \/\/ базовая точка 1<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.y = 39.95; \/\/<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.ang = 0.00; \/\/ угол вектора направления от 1-ой точки ко 2-ой<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.height = -66.68; \/\/ высота<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.weight = 79.90; \/\/ ширина<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.pCorner = ::CreateArray(CORNER_ARR, 0); \/\/ Создание массива параметров углов<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">par.style = 1; \/\/ стиль линии<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">::ksRectangle(&amp;par, 0); \/\/ создать прямоугольник<\/span><\/p>\n\r"
})
