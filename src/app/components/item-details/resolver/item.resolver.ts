import { inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { CatalogService } from "../../../services/catalog.service";

export const itemResolver: ResolveFn<Object> = (route, state) => {
    return inject(CatalogService).getById(route.paramMap.get('id')!);
}