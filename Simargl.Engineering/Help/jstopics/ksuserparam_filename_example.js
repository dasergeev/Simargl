hmLoadTopic({
hmKeywords:"",
hmTitle:"ksUserParam::fileName - пример использования",
hmDescription:"Получить имя файла пользовательской библиотеки, при помощи которой можно",
hmPrevLink:"",
hmNextLink:"",
hmParentLink:"compiling_libraries.html",
hmBreadCrumbs:"",
hmTitlePath:"Компиляция библиотек > Использование библиотек типов интерфейсов Системы КОМПАС-3D",
hmHeader:"<h1 class=\"p_Heading1\"><span class=\"f_Heading1\">ksUserParam::fileName - пример использования <\/span><\/h1>\n\r",
hmBody:"<p class=\"p_bodytext\"><a id=\"xcq1950825\" class=\"hmanchor\"><\/a><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">Получить имя файла пользовательской библиотеки, при помощи которой можно<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">редактировать компонент.<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/получить интерфейс ksUserParam у KompasObject<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksUserParam userParam(kompasObject.GetParamStruct(ko_UserParam));<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">userParam.Init();<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/заполнить параметры интерфейса<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">ksPart.GetUserParam(userParam);<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">\/\/Получить имя файла пользовательской библиотеки<\/span><\/p>\n\r<p class=\"p_bodytext\"><span class=\"f_bodytext\">BSTR userLibraryFileName = userParam.fileName;<\/span><\/p>\n\r"
})
