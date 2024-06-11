import { Episode } from "./episode.model";
import { Genre } from "./genre.model";

export interface Title {
    id: number;
    thumbnail: string;
    name: string;
    description: string;
    release: number;
    episodes: Episode[];
    genres: Genre[];
}