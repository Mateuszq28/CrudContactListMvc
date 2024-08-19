// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    // ====================
    // UNIVERSAL AJAX TOOLS
    // ====================
    
    
    // console debug log wrapper
    function myLog(val) {
        if (true) {
            console.log(val);
        }
    }


    myLog("Page is ready");


    // update HTML element by its id
    function updateElement(id, data_to_put) {
        // ------- block to delete --------
        // |                              |
        myLog("--updateElement--");
        myLog("data_to_put:");
        myLog(data_to_put);
        // |                              |
        // ------- block to delete --------
        $(id).html(data_to_put)
    }


    // POST data from your form (button, submit)
    // user HTML interface --> POST to server
    // CATCH response with AJAX
    // AJAX <-- GET from server
    // AJAX --> response
    function getPartialViewAjax(url, data_in, arr_id_put_response) {
        $.ajax({
            type: "POST",
            url: url,
            data: data_in,
            success: function (response) {
                // ------- block to delete --------
                // |                              |
                myLog("--getPartialViewAjax--");
                myLog("data posted to the controller (data_in):");
                myLog(data_in);
                // |                              |
                // ------- block to delete --------
                // ------- block to delete --------
                // |                              |
                myLog("--getPartialViewAjax--");
                myLog("url:");
                myLog(url);
                // |                              |
                // ------- block to delete --------
                // ------- block to delete --------
                // |                              |
                myLog("--getPartialViewAjax--");
                myLog("response:");
                myLog(response);
                // |                              |
                // ------- block to delete --------
                arr_id_put_response.forEach((id_put_response) => updateElement(id=id_put_response, data_to_put=response));
                // ------- block to delete --------
                // |                              |
                myLog("--getPartialViewAjax--");
                myLog("Elements updated:");
                myLog(arr_id_put_response);
                // |                              |
                // ------- block to delete --------
            }
        });
    }


    // USE THIS TO AJAX, ALL-IN-ONE
    function ajaxWrapper(id_button_submit,
                         id_collecting_form,
                         url_server,
                         arr_id_put_response
    ) {
        // Annotation to this line:
        // $(id_button_submit).click(function (event) {
        // to work with <a> tags disable all $("a"),
        // usuing only #id with no "disabling" doesn't work
        $(id_button_submit).click(function (event) {
            event.preventDefault();
            data_in = $(id_collecting_form);
            data_in_serialized = data_in.serialize();
            // ------- block to delete --------
            // |                              |
            myLog("--ajaxWrapper--");
            myLog("select button was clicked");
            myLog("data from the form:");
            myLog(data_in);
            myLog("data serialized:");
            myLog(data_in_serialized);
            // |                              |
            // ------- block to delete --------
            getPartialViewAjax(url_server, data_in_serialized, arr_id_put_response);
            // ------- block to delete --------
            // |                              |
            myLog("--ajaxWrapper--");
            myLog("end of ajaxWrapper");
            // |                              |
            // ------- block to delete --------
        });
    }




    // ==========================
    // TOOLS PRACTICAL APLICATION
    // ==========================


    // +++++++++++++++++++++++
    // MAIN PAGE (Simps/Index)
    // +++++++++++++++++++++++


    // UPDATE MANY PLACES AT ONCE
    Arr_Contact_Index_ids = ["#Contact_Multiblock_getIndex_content",
                             "#Contact_getIndex_content"
                            ];
    Arr_Contact_Create_ids = ["#Contact_Multiblock_getIndex_content",
                             "#Contact_getCreate_content"
                            ];
    Arr_Contact_Edit_ids = ["#Contact_Multiblock_getIndex_content",
                             "#Contact_getEdit_content"
                            ];
    Arr_Contact_Details_ids = ["#Contact_Multiblock_getIndex_content",
                             "#Contact_getDetails_content"
                            ];
    Arr_Contact_Delete_ids = ["#Contact_Multiblock_getIndex_content",
                             "#Contact_getDelete_content"
                            ];


    // SPA (old)
    // Single Page Application - matryoshka
    // Contact Index, Block 1
    ajaxWrapper(id_button_submit = "#buttonGetContactIndex",
                id_collecting_form = "#form",
                url_server = "Spas/ShowContactIndex",
                arr_id_put_response = ["#contactIndexContent"]
                );


    // MAINE PAGE (new)


    // Contact Index, Multiblock
    ajaxWrapper(id_button_submit = "#Contact_Multiblock_getIndex",
                id_collecting_form = "#Contact_Multiblock_form_getIndex",
                url_server = "Simps/Contact_Index",
                arr_id_put_response = ["#Contact_Multiblock_getIndex_content"]
                );


    // Contact Index, Block 1
    ajaxWrapper(id_button_submit = "#Contact_getIndex",
                id_collecting_form = "#Contact_form_getIndex",
                url_server = "Simps/Contact_Index",
                arr_id_put_response = ["#Contact_getIndex_content"]
                );


    // Contact Create, Block 2
    ajaxWrapper(id_button_submit = "#Contact_getCreate",
                id_collecting_form = "#Contact_form_getCreate",
                url_server = "Simps/Contact_Create",
                arr_id_put_response = ["#Contact_getCreate_content"]
                );


    // Contact Details, Block 3 - only from Contact Index


    // Contact Edit, Block 4 - only from Contact Index


    // Contact Delete, Block 5 - only from Contact Index


    // Contact Search, Block 6
    ajaxWrapper(id_button_submit = "#Contact_getSearch",
                id_collecting_form = "#Contact_form_getSearch",
                url_server = "Simps/Contact_Search",
                arr_id_put_response = ["#Contact_getSearch_content"]
                );

    
    // +++++++++++++++++++
    // Simps/Contact_Index
    // +++++++++++++++++++

    
    // Partial Contact Index
    // (in Arr...): #Contact_getCreate_content is in main page (Index)
    // Create
    ajaxWrapper(id_button_submit = "#Partial_Contact_getCreate",
        id_collecting_form = "#Partial_Contact_form_getCreate",
        url_server = "Simps/Contact_Create",
        arr_id_put_response = Arr_Contact_Create_ids
        );

    
    // Edit
    ajaxWrapper(id_button_submit = "#Partial_Contact_getEdit",
        id_collecting_form = "#Partial_Contact_form_getEdit",
        url_server = "Simps/Contact_Edit",
        arr_id_put_response = Arr_Contact_Edit_ids
        );

    
    // Details
    ajaxWrapper(id_button_submit = "#Partial_Contact_getDetails",
        id_collecting_form = "#Partial_Contact_form_getDetails",
        url_server = "Simps/Contact_Details",
        arr_id_put_response = Arr_Contact_Details_ids
        );

    
    // Delete
    ajaxWrapper(id_button_submit = "#Partial_Contact_getDelete",
        id_collecting_form = "#Partial_Contact_form_getDelete",
        url_server = "Simps/Contact_Delete",
        arr_id_put_response = Arr_Contact_Delete_ids
        );


});