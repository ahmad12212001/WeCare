import { AbstractControl, FormGroup, ValidatorFn, Validators } from "@angular/forms";

export function requiredWhenIsNull(form: FormGroup): ValidatorFn {
    if (form) {
        const dependentControlValue = form.controls["exam"].value;
        if (dependentControlValue == null) {
            form.controls["requestType"].setValidators(Validators.required);

            return (control: AbstractControl): { [key: string]: any } | null => {

                return { required: true }
            }
        }

        if (form.controls["requestType"].hasValidator(Validators.required)) {
            form.controls["requestType"].removeValidators(Validators.required);
        }
    }
    return null;

}