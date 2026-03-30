hmLoadTopic({
hmKeywords:"",
hmTitle:"Зависимость параметра от типа компиляции",
hmDescription:"В зависимости от типа компиляции используется как ANSI или Unicode вариант.",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">Зависимость параметра от типа компиляции <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><span class=\"f_bodytext\">В зависимости от типа компиляции используется как ANSI или Unicode вариант.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">При использовании в проекте предопределенного определения _UNICODE, предназначенного для компиляции проекта под Unicode, константе присваивается значение, заданное с в объявлении с суффиксом W.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Например:<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">#ifdef _UNICODE<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">#define ALLPARAM_T ALLPARAM_W<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">#else<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">#define ALLPARAM_T ALLPARAM<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">#endif \/\/ !UNICODE<\/span><\/p>\n\r"
})
