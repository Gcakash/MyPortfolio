export interface BlogModel
{
 blogId: number;
 title: string;
 content : string;
 image : string;
 datePublished? : Date;
 isActive? : boolean;
}