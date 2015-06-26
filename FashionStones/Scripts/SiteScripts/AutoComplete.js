        $(document).ready(function () {
            $("#search_input").autocomplete({
                minLength: 3,
               
                source: function (request, response) {
                    $.ajax({
                        url: "/Home/AutoCompleteCountry",
                        type: "POST",
                        dataType: "json",
                        async: false,
                        cache: false,
                        data: { term: request.term },
                        success: function(data) {
                            response($.map(data, function(item) {
                                return { label: item.Name, value: item.Name };
                            }));

                        }
                    });
                },
                messages: {
                    noResults: "", results: ""
                },
                select: function(event, ui) {
                    console.log(ui);
                }
            });
        })