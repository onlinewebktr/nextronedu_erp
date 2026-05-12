function calulate_incomehead() {

    $.each(incomes, function (i, item) {
        if (item.IsVeriable == 'Yes') {
            var amount = get_calculation_on(item.CalculationBassedOn)
            if (item.PercentageValue.includes('%')) {
                item.inc = amount * parseFloat(item.PercentageValue) / 100;
            }
            else {
                item.inc = parseFloat(item.PercentageValue)
            }
            item.inc = Math.round(item.inc)
        }
    });
}
function get_calculation_on(itemid) {
    var matchingInc = null;
    $.each(incomes, function (i, item) {
        if (item.IncomeHeadId == itemid) {
            matchingInc = item.inc;
            return false; // exit the loop early since we found a match
        }
    });
    return matchingInc;
}
function calulate_deduction() {
    var basic = 0
    var gross = 0;
    var total_deduct = 0;
    var emp_contri = 0;
    $.each(incomes, function (i, item) {
        if (item.IsBasic == 'Yes') {
            basic += parseFloat(item.inc)
        }
        gross += parseFloat(item.inc)
    });
    $.each(deductions, function (i, item) {
        console.log(item)
        item.deduct = 0;
        item.deduct_emp = 0;
        item.allow_edit = false;
        if (item.ApplyOn == 'Basic Salary') {
            calc_deduc_part1(item, basic)
        }
        else if (item.ApplyOn == 'Gross Salary') {
            calc_deduc_part1(item, gross)
        }
        else if (item.ApplyOn == 'Gross Salary Bellow') {
            if (parseInt(item.BassedOn) >= gross) {
                calc_deduc_part1(item, gross)
            }
        }
        else if (item.ApplyOn == 'Basic Salary Bellow') {
            if (parseInt(item.BassedOn) >= basic) {
                calc_deduc_part1(item, basic)
            }
        }
        else if (item.ApplyOn == 'Basic Salary Above') {
            if (parseInt(item.BassedOn) < basic) {
                calc_deduc_part1(item, basic)
            }
        }
        else if (item.ApplyOn == 'Gross Salary Above') {
            if (parseInt(item.BassedOn) < gross) {
                calc_deduc_part1(item, gross)
            }
        }
        total_deduct += item.deduct
        emp_contri += item.deduct_emp
    });
    $('#Gross').val(gross);
    $('#Deduction').val(total_deduct);
    $('#Net_Salary').val(gross - total_deduct);
    $('#Employeer_Contribution').val(emp_contri);
    $('#CTC_month').val(gross + emp_contri);
    $('#CTC_year').val((gross + emp_contri) * 12);
}

function calc_deduc_part1(item, amount) {
    if (item.isEnable == false) {
        item.deduct = 0;
        item.deduct_emp = 0;
        item.allow_edit = false;
        return false;
    }
    if (item.DeductionType == "PF") {
        if (amount > 15000) {
            amount = 15000;
        }

    }
    if (item.Employee_Contribution.includes('%')) {
        item.deduct = amount * parseFloat(item.Employee_Contribution) / 100;
    }
    else {
        if (item.custum_deduct) {
            item.deduct = parseFloat(item.custum_deduct)
        }
        else {
            item.deduct = parseFloat(item.Employee_Contribution)
        }
        item.allow_edit = true;
    }
    if (item.deduct > parseFloat(item.Max_Allowed) && parseFloat(item.Max_Allowed) > 0) {
        item.deduct = parseFloat(item.Max_Allowed)
    }
    if (item.IsDeductionOnBoth == 'Both') {
        

        if (item.Employer_Contribution.includes('%')) {
            item.deduct_emp = amount * parseFloat(item.Employer_Contribution) / 100;
        }
        else {
            item.deduct_emp = parseFloat(item.Employer_Contribution)
        }
    }

    if (item.DeductionType == 'ESI') {
        var round = Math.round(item.deduct); //ex 3.25 -> 3
        if (item.deduct > round) {
            //item.deduct = round + 1;//4
            item.deduct = round;

        }
        else {
            item.deduct = round;
        }
        var round1 = Math.round(item.deduct_emp);
        console.log(item.deduct_emp);
        console.log(round1);
        if (item.deduct_emp > round1) {

           // item.deduct_emp = round1 + 1;
            item.deduct_emp = round1;
        }
        else {
            item.deduct_emp = round1;
        }
    }
    else {
        item.deduct = Math.round(item.deduct);


        item.deduct_emp = Math.round(item.deduct_emp);
    }
}
function bind_income_table() {
    var tableBody = $('#tbl-income tbody');
    tableBody.empty();

    $.each(incomes, function (index, d) {
        var editing = "disabled";
        if (d.IsVeriable == 'No') {
            editing = "";
        }
        if (d.MaximumValue > 0) {
            editing += ' placeholder="Max : ' + d.MaximumValue + '" ';
        }
        var row = $('<tr>').append(
            $('<td>').text(index + 1),
            $('<td>').text(d.IncomeHead),
            $('<td>').append('<input type="number"  ' + editing + '  value="' + d.inc + '" data-max="' + d.MaximumValue + '"  class="form-control edit-text"  data-id="' + index + '">')
        );
        tableBody.append(row);
    });
}
function bind_deduction_table() {
    var tableBody = $('#tbl-deduct tbody');
    tableBody.empty();


    $.each(deductions, function (index, d) {
        var editing = "disabled";
        if (d.allow_edit) {
            editing = "";
        }
        if (d.Max_Allowed > 0) {
            editing += ' placeholder="Max : ' + d.Max_Allowed + '" ';
        }
        var desc = $('<span>').append(d.DeductionDesc);
        
        if (d.ApplyOn == "Basic Salary") {
            desc.append('<br/>').append($('<span style="font-size:12px">').append(d.Employee_Contribution).append(' of ').append(d.ApplyOn));
        }
        else {
            desc.append('<br/>').append($('<span style="font-size:12px">').append(d.ApplyOn).append(' ').append(d.BassedOn));
        }
        var row = $('<tr>').append(
            $('<td>').text(index + 1),
            $('<td>').append(desc),
            $('<td>').append('<input type="number" ' + editing + ' value="' + d.deduct + '" class="form-control edit-deduct"  data-id="' + index + '" data-max="' + d.Max_Allowed + '" >'),
            $('<td>').append('<input type="number" disabled value="' + d.deduct_emp + '" class="form-control"  data-did="' + index + '">'),
            $('<td>').append($('<input type="checkbox" class="chk_enbled"   data-did="' + index + '">').prop('checked', d.isEnable))
        );
        tableBody.append(row);
    });
}
 
$('#tbl-income').on('input', '.edit-text', function () {

    var ind = parseInt($(this).data('id'));
    console.log(ind);
    var max = parseInt($(this).data('max'));

    var vale = parseInt($(this).val());
    if (isNaN(vale)) {
        vale = 0;
    }
    if (max > 0 && vale > max) {
        $(this).val(max)
        vale = max;
    }
    incomes[ind].inc = vale;
    calulate_incomehead()
    calulate_deduction()
    $.each(incomes, function (index, d) {
        if (ind == index) {

        }
        else {
            var element = $('[data-id="' + index + '"]');
            element.val(d.inc);
        }
    });
    // bind_income_table()
    bind_deduction_table()
    // $(this).focus();
})
$('#tbl-deduct').on('input', '.edit-deduct', function () {

    var ind = parseInt($(this).data('id'));
    console.log(ind);
    var max = parseInt($(this).data('max'));

    var vale = parseInt($(this).val());
    if (isNaN(vale)) {
        vale = 0;
    }
    if (max > 0 && vale > max) {
        $(this).val(max)
        vale = max;
    }
    deductions[ind].deduct = vale;
    deductions[ind].custum_deduct = vale;

    calulate_deduction()
})
$('#tbl-deduct').on('change', '.chk_enbled', function () {

    var ind = parseInt($(this).data('did'));
    console.log(ind);
    deductions[ind].isEnable = $(this).prop("checked");

    calulate_deduction()
    bind_deduction_table()
})
