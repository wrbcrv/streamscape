import { Episode } from "./episode.model";
import { Genre } from "./genre.model";

export interface Title {
    id: number;
    name: string;
    release: number;
    episodes: Episode[];
    genres: Genre[];
}